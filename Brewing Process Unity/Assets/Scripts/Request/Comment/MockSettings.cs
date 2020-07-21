using System;
using UnityEngine;

public class MockSettings : MonoBehaviour
{
    void Start()
    {
        var integration = FindObjectOfType<CommentIntegration>();
        
        integration.GetComments(Guid.Parse("759b1b42-f0cc-4b17-bb57-698dcbaded10"));
    }
}
