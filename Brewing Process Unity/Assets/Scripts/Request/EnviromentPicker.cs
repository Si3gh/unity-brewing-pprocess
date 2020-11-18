using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enviroment", order = 2)]
public class EnviromentPicker : ScriptableObject
{
    [SerializeField] private Env enviroment = Env.prod;
    [SerializeField] private RequestUrl prodUrl;
    [SerializeField] private RequestUrl stagingUrl;
    [SerializeField] private RequestUrl devUrl;
    [SerializeField] private RequestUrl localUrl;

    public RequestUrl GetUrl()
    {
        RequestUrl returnedUrl = null;

        switch (enviroment)
        {
            case Env.prod: returnedUrl = prodUrl; break;
            case Env.staging: returnedUrl = stagingUrl; break;
            case Env.dev: returnedUrl = devUrl; break;
            case Env.local: returnedUrl = localUrl; break;
        }

        return returnedUrl;
    }

    private enum Env
    {
        prod, dev, staging, local
    }

}