using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private bool isOpen;

    [SerializeField] private Quaternion closedRotation;
    [SerializeField] private Quaternion openRoatation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = transform.rotation;
        openRoatation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation;

        if(isOpen)
        {
            targetRotation = openRoatation;
        }
        else
        {
            targetRotation = closedRotation;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    public void Interact()
    {
        isOpen = !isOpen;                   //bool 값을 뒤짚는다
    }

    public string GetGuideText()
    {
        if(isOpen)
        {
            return "[E] 문 닫기";
        }
        return "[E] 문 열기";
    }
}
