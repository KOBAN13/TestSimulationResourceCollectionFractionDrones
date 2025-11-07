using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class DroneSimulationView : MonoBehaviour
    { 
        [SerializeField] private Slider _droneCountInRedTeamSlider;
        [SerializeField] private Slider _droneCountInBlueTeamSlider;
        [SerializeField] private Slider _droneSpeedSkider;
        [SerializeField] private TMP_Text _droneCountTextInRedTeam;
        [SerializeField] private TMP_Text _droneCountTextInBlueTeam;
        [SerializeField] private TMP_Text _droneSpeedText;
        [SerializeField] private TMP_Text _resourcesСollectedRedTeam;
        [SerializeField] private TMP_Text _resourcesСollectedBlueTeam;

        [SerializeField] private TMP_InputField _resourceSpawnGeneration;
        [SerializeField] private Toggle _showDronePath;

        private DroneSimulationPresenter _presenter;

        public event Action<int> OnDroneCountRedTeamChanged;
        public event Action<int> OnDroneCountBlueTeamChanged;
        public event Action<string> OnResourceSpawnGenerationChanged;
        public event Action<float> OnDroneSpeedChanged;
        public event Action<bool> OnShowDronePath;

        [Inject]
        public void Construct(DroneSimulationPresenter presenter)
        {
            _presenter = presenter;
        }

        private void Start()
        {
            InitializeUI();
            _presenter.Initialize(this);
        }

        private void OnDestroy()
        {
            UnsubscribeFromUI();
            _presenter.Dispose();
        }

        private void InitializeUI()
        {
            _showDronePath.onValueChanged.AddListener(OnDronePathToggleChanged);
            _droneCountInBlueTeamSlider.onValueChanged.AddListener(OnDroneSliderInBlueTeamChanged);
            _droneCountInRedTeamSlider.onValueChanged.AddListener(OnDroneSliderInRedTeamChanged);
            _droneSpeedSkider.onValueChanged.AddListener(OnSpeedSliderChanged);
            _resourceSpawnGeneration.onEndEdit.AddListener(OnResourceSpawnGenerationInputFieldChanged);
        }

        private void UnsubscribeFromUI()
        {
            _showDronePath.onValueChanged.RemoveListener(OnDronePathToggleChanged);
            _droneCountInBlueTeamSlider.onValueChanged.RemoveListener(OnDroneSliderInBlueTeamChanged);
            _droneCountInRedTeamSlider.onValueChanged.RemoveListener(OnDroneSliderInRedTeamChanged);
            _droneSpeedSkider.onValueChanged.RemoveListener(OnSpeedSliderChanged);
            _resourceSpawnGeneration.onEndEdit.RemoveListener(OnResourceSpawnGenerationInputFieldChanged);
        }

        private void OnDronePathToggleChanged(bool isActive)
        {
            OnShowDronePath?.Invoke(isActive);
        }

        private void OnDroneSliderInRedTeamChanged(float value)
        {
            var count = Mathf.RoundToInt(value);
            OnDroneCountRedTeamChanged?.Invoke(count);
        }
        
        private void OnDroneSliderInBlueTeamChanged(float value)
        {
            var count = Mathf.RoundToInt(value);
            OnDroneCountBlueTeamChanged?.Invoke(count);
        }

        private void OnSpeedSliderChanged(float value)
        {
            var speed = Mathf.RoundToInt(value);
            OnDroneSpeedChanged?.Invoke(speed);
        }
        
        private void OnResourceSpawnGenerationInputFieldChanged(string value)
        {
            OnResourceSpawnGenerationChanged?.Invoke(value);
        }

        public void UpdateDroneCountInRedTeamDisplay(int count)
            => _droneCountTextInRedTeam.text = count.ToString();
        
        public void UpdateDroneCountInBlueTeamDisplay(int count)
            => _droneCountTextInBlueTeam.text = count.ToString();

        public void UpdateDroneSpeedDisplay(float speed)
            => _droneSpeedText.text = speed.ToString("F1");
        
        public void UpdateResourcesCollectedRedTeamDisplay(int count)
            => _resourcesСollectedRedTeam.text = count.ToString();
        
        public void UpdateResourcesCollectedBlueTeamDisplay(int count)
            => _resourcesСollectedBlueTeam.text = count.ToString();
    }
}