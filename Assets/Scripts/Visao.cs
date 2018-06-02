using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visao : MonoBehaviour {

    public float DistanciaVisao;
    [Range(0, 360)]
    public float AnguloVisao;
    [Tooltip("Tempo que o inimigo demora até perceber que o player está dentro do campo de visão")]
    public float TempoReacaoInimigo = .2f;

    [Tooltip("Layer do player quando for visto pelo inimigo")]
    public LayerMask playerLayer;
    [Tooltip("Layer dos obstaculos que são vistos pelo inimigo")]
    public LayerMask obstacleLayer;

    public float meshResolution = 2.0f;
    public MeshFilter viewMeshFilter;

    [Tooltip("O quão fluido você quer que seja a visão no inimigo com obstaculos e afins?")]
    public int edgeResolveIterations = 6;

    [Tooltip("Distancia máxima de um obstaculo a outro que permite a visão escolher qual priorizar seu raio")]
    public float edgeDstThreshold = 0.5f;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private Mesh _viewMesh;

    private void Start()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = _viewMesh;
        StartCoroutine("FindTargetWithdelay", TempoReacaoInimigo);
    }

    // É chamado depois de atualizar a rotação do inimigo
    private void LateUpdate()
    {
        DrawFieldOfView();
    }
    // Dá um tempo de espera até relamente validar que o player está no campo de visão
    IEnumerator FindTargetWithdelay (float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, DistanciaVisao, playerLayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

             // Verifica o angulo entre a direção que o inimigo está olhando e a a distancia do player pro inimigo, 
             // se o angulo for menor que o angulo de visão do inimigo significa que ele está no angulo de visão, got ya
             if (Vector3.Angle (transform.forward, directionToTarget) < AnguloVisao/2)
             {
                 float distanceToTarget = Vector3.Distance(transform.position, target.position);

                 // Verifica se tem algum obstaculo no caminho
                 if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                 {
                     visibleTargets.Add(target);
                    GetComponentInParent<Enemy>().AttackPlayer();
                 }
             }
        }
    }

    // Cria triangulações entre cada vertice de pontos do raycast
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(AnguloVisao * meshResolution);
        float stepAngleSize = AnguloVisao / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();


        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - AnguloVisao / 2 + stepAngleSize * i;
            ViewCastInfo newViewcast = viewCast(angle);

            // verifica se encostou em um objeto ou fora
            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewcast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewcast.hit || (oldViewCast.hit && newViewcast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewcast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewcast.point);
            oldViewCast = newViewcast;
        }

        // + 1 é vertice de origem
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero; // origem
        for (int i = 0; i < vertexCount - 1; i++)   // forma os triangulos
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewcast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewcast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i =0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = viewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewcast.dst - newViewCast.dst) > edgeDstThreshold;

            if (newViewCast.hit == minViewcast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            } else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo viewCast(float globalAngle)
    {
        Vector3 dir = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position,dir,out hit, DistanciaVisao, obstacleLayer))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        } else
        {
            return new ViewCastInfo(false, transform.position + dir * DistanciaVisao, DistanciaVisao, globalAngle);
        }
    }

    // Angulos no Unity são clockwise então (x= 90 - norte até a direita)
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // constructor com info sobre o raycast da visão
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    // Metodo usado para encontra a ponta de obstáculos e 
    // conseguir redimensionar o aspectro de visão do inimigo para que fique fluido
    public struct EdgeInfo
    {
        public Vector3 pointA; //ponto que atinge o obstáculo
        public Vector3 pointB; //primeiro ponto fora do obstáculo

        public EdgeInfo(Vector3 _pointA,Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }




    /*public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            print("Player Morreu =(");
    }*/

    
}
