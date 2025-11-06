using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class DroneSimulationView : MonoBehaviour
    { 
        [SerializeField] private Slider _droneCountSlider;
        [SerializeField] private Slider _droneSpeedSkider;
        [SerializeField] private TMP_Text _droneCountText;
        [SerializeField] private TMP_Text _droneSpeedText;

        [SerializeField] private TMP_InputField _resourceSpawnGeneration;
        [SerializeField] private Toggle _showDronePath;

        private DroneSimulationPresenter _presenter;

        public event Action<int> OnDroneCountChanged;
        public event Action<float> OnDroneSpeedChanged;

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
            _droneCountSlider.onValueChanged.AddListener(OnDroneSliderChanged);
            _droneSpeedSkider.onValueChanged.AddListener(OnSpeedSliderChanged);
        }

        private void UnsubscribeFromUI()
        {
            _droneCountSlider.onValueChanged.RemoveListener(OnDroneSliderChanged);
            _droneSpeedSkider.onValueChanged.RemoveListener(OnSpeedSliderChanged);
        }

        private void OnDroneSliderChanged(float value)
        {
            var count = Mathf.RoundToInt(value);
            OnDroneCountChanged?.Invoke(count);
        }

        private void OnSpeedSliderChanged(float value)
        {
            var speed = Mathf.RoundToInt(value);
            OnDroneSpeedChanged?.Invoke(speed);
        }

        public void UpdateDroneCountDisplay(int count)
        {
            _droneCountText.text = count.ToString();
        }

        public void UpdateDroneSpeedDisplay(float speed)
        {
            _droneSpeedText.text = speed.ToString("F1");
        }
    }
}