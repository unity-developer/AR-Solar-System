using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;


public class InformationBtnController : MonoBehaviour
{
    //This object is contain the planet information
    public ItemsJsonSerilization itemsJsonSerilization;

    //Vuforia TargetImages
    public GameObject[] VuforiaTargetImage;


    //Planet Information panel 
    public GameObject PlanetInfoPanel;

    //Home Buttons
    public GameObject BackBtn;
    public GameObject _downloadBtn;
    public GameObject _flashBtn;
    public GameObject _infoBtn;
    public GameObject _searchPlanetImage;

    int BasicInfoTextCount = 0;
   
    public static int i;

    //Planet Name Text object
    public TextMeshProUGUI _namePanel_text;


    //private Content
    private TextMeshProUGUI _basictext;
    private TextMeshProUGUI _fulltext;
    private TextMeshProUGUI _masstext;
    private TextMeshProUGUI _volumetext;
    private TextMeshProUGUI _gravitytext;
    private TextMeshProUGUI _oneyeartext;

    //3D object parent gameobject
    public GameObject Info3dPlanets;

    void Start()
    {
        _basictext = PlanetInfoPanel.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        _fulltext = PlanetInfoPanel.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        _masstext = PlanetInfoPanel.transform.GetChild(3).transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        _volumetext = PlanetInfoPanel.transform.GetChild(3).transform.GetChild(2).transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        _gravitytext = PlanetInfoPanel.transform.GetChild(3).transform.GetChild(2).transform.GetChild(2).transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        _oneyeartext = PlanetInfoPanel.transform.GetChild(3).transform.GetChild(2).transform.GetChild(2).transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {

        if (DefaultTrackableEventHandler.checktracking)
        {            
            _infoBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DefaultTrackableEventHandler.CurrentMarkerName;
            _infoBtn.GetComponent<Animator>().SetBool("InfoBtn", true);
            
        }
        else
        {
            _infoBtn.GetComponent<Animator>().SetBool("InfoBtn", false);

        }

    }

    public void PlanetInfoFun()
    {
        
        for (int i = 0; i < VuforiaTargetImage.Length; i++)
        {
            VuforiaTargetImage[i].SetActive(false);

        }
        for (int i = 0; i < 9; i++)
        {
            if (itemsJsonSerilization.items[i].PlanetName == DefaultTrackableEventHandler.CurrentMarkerName)
            {
                _basictext.text = itemsJsonSerilization.items[i].basicInformation.context[0];
            }
        }

        for (int i=0;i<9;i++)
        {
            if(Info3dPlanets.transform.GetChild(i).gameObject.name==DefaultTrackableEventHandler.CurrentMarkerName)
            {
                Info3dPlanets.transform.GetChild(i).gameObject.SetActive(true);

                Debug.Log(Info3dPlanets.transform.GetChild(i).gameObject.name);
                _namePanel_text.text = Info3dPlanets.transform.GetChild(i).gameObject.name;
                PlanetInfoPanel.GetComponent<Animator>().SetBool("InfoPanel", true);
                PlanetInfoPanel.GetComponent<Animator>().SetBool("BasicInfoPanel", true);
                BackBtn.SetActive(true);
                _downloadBtn.SetActive(false);
                _flashBtn.SetActive(false);
                _infoBtn.SetActive(false);
                _searchPlanetImage.SetActive(false);
              

            }
        }
        

    }

    public  void   InfoBtnEnable(string name)
    {
        _infoBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        _infoBtn.GetComponent<Animator>().SetBool("InfoBtn", true);
    }
    public void InfoBtnDisable()
    {
        _infoBtn.GetComponent<Animator>().SetBool("InfoBtn", false);
    }
    public void BackFun()
    {
        _namePanel_text.text = Info3dPlanets.transform.GetChild(i).gameObject.name;
        PlanetInfoPanel.GetComponent<Animator>().SetBool("InfoPanel", false);
        BackBtn.SetActive(false);
        StartCoroutine(HomeButtonsEnable());
        for (int i = 0; i < VuforiaTargetImage.Length; i++)
        {
            VuforiaTargetImage[i].SetActive(true);
        }
        for (int i = 0; i < Info3dPlanets.transform.childCount; i++)
        {
            Info3dPlanets.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    IEnumerator HomeButtonsEnable()
    {
        yield return new WaitForSeconds(0.5f);
        _downloadBtn.SetActive(true);
        _flashBtn.SetActive(true);
        _infoBtn.SetActive(true);
        _searchPlanetImage.SetActive(true);
    }

    public void BasicInfoFun()
    {
        for (int i = 0; i < 9; i++)
        {
            if (itemsJsonSerilization.items[i].PlanetName == DefaultTrackableEventHandler.CurrentMarkerName)
            {
                _basictext.text = itemsJsonSerilization.items[i].basicInformation.context[0];
            }
        }
        BasicInfoTextCount = 1;
        PlanetInfoPanel.GetComponent<Animator>().SetBool("MeasurmentPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("GalleryPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("FullInfoPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("BasicInfoPanel", true);


    }
    public void BasiINfotextChange()
    {

        for (int i = 0; i < 9; i++)
        {
            if (itemsJsonSerilization.items[i].PlanetName == DefaultTrackableEventHandler.CurrentMarkerName)
            {
                if (BasicInfoTextCount < itemsJsonSerilization.items[i].basicInformation.context.Length)
                {
                    _basictext.text = itemsJsonSerilization.items[i].basicInformation.context[BasicInfoTextCount];

                }
                else
                {
                    BasicInfoTextCount = 0;
                }
            }
        }
        BasicInfoTextCount++;
               
    }

    public void FullInfoFun()
    {
        for (int i = 0; i < 9; i++)
        {
            if (itemsJsonSerilization.items[i].PlanetName == DefaultTrackableEventHandler.CurrentMarkerName)
            {
                _fulltext.text = itemsJsonSerilization.items[i].fullInformation.text;


            }
        }

        PlanetInfoPanel.GetComponent<Animator>().SetBool("GalleryPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("BasicInfoPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("MeasurmentPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("FullInfoPanel", true);

    }

    public void MeasurmentFun()
    {

        for (int i = 0; i < 9; i++)
        {
            if (itemsJsonSerilization.items[i].PlanetName == DefaultTrackableEventHandler.CurrentMarkerName)
            {
                _masstext.text = itemsJsonSerilization.items[i].measurmentDetails.mass;
                _volumetext.text = itemsJsonSerilization.items[i].measurmentDetails.Volume;
                _gravitytext.text = itemsJsonSerilization.items[i].measurmentDetails.gravity;
                _oneyeartext.text = itemsJsonSerilization.items[i].measurmentDetails.oneyear;


            }
        }
        PlanetInfoPanel.GetComponent<Animator>().SetBool("GalleryPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("FullInfoPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("BasicInfoPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("MeasurmentPanel", true);
        
    }


    public void GalleryFun()
    {
        PlanetInfoPanel.GetComponent<Animator>().SetBool("FullInfoPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("BasicInfoPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("MeasurmentPanel", false);
        PlanetInfoPanel.GetComponent<Animator>().SetBool("GalleryPanel", true);
    }

}
