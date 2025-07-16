using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using MyOfficeEquipmentInventory.Models;

/// <summary>
/// Репозиторий для работы с данными об оборудовании
/// </summary>
public class EquipmentRepository
{
    private const string FilePath = "equipment.json";
    private ObservableCollection<Equipment> _equipments;

    /// <summary>
    /// Конструктор репозитория. Загружает данные или инициализирует тестовыми значениями
    /// </summary>
    public EquipmentRepository()
    {
        LoadData();

        // Инициализация тестовыми данными, если файл пустой
        if (!_equipments.Any())
        {
            _equipments = new ObservableCollection<Equipment>
            {
                new Equipment { Id = 1, Name = "HP LaserJet Pro", Type = EquipmentType.Printer, Status = StatusType.InUse },
                new Equipment { Id = 2, Name = "Epson V600", Type = EquipmentType.Scanner, Status = StatusType.InStock },
                new Equipment { Id = 3, Name = "Dell UltraSharp 27\"", Type = EquipmentType.Monitor, Status = StatusType.InRepair }
            };
            SaveData();
        }
    }

    /// <summary>
    /// Загрузка данных из JSON-файла
    /// </summary>
    private void LoadData()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            _equipments = JsonConvert.DeserializeObject<ObservableCollection<Equipment>>(json);
        }
        else
        {
            _equipments = new ObservableCollection<Equipment>();
        }
    }

    /// <summary>
    /// Сохранение данных в JSON-файл
    /// </summary>
    private void SaveData()
    {
        var json = JsonConvert.SerializeObject(_equipments, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    /// <summary>
    /// Получение всего списка оборудования
    /// </summary>
    public ObservableCollection<Equipment> GetAllEquipments() => _equipments;

    /// <summary>
    /// Добавление нового оборудования
    /// </summary>
    /// <param name="equipment">Объект оборудования для добавления</param>
    public void AddEquipment(Equipment equipment)
    {
        equipment.Id = GetNextId();
        _equipments.Add(equipment);
        SaveData();
    }

    /// <summary>
    /// Обновление данных оборудования
    /// </summary>
    /// <param name="equipment">Объект с обновленными данными</param>
    public void UpdateEquipment(Equipment equipment)
    {
        var existing = _equipments.FirstOrDefault(e => e.Id == equipment.Id);
        if (existing != null)
        {
            existing.Name = equipment.Name;
            existing.Type = equipment.Type;
            existing.Status = equipment.Status;
            SaveData();
        }
    }

    /// <summary>
    /// Удаление оборудования по ID
    /// </summary>
    /// <param name="id">ID оборудования для удаления</param>
    public void DeleteEquipment(int id)
    {
        var equipment = _equipments.FirstOrDefault(e => e.Id == id);
        if (equipment != null)
        {
            _equipments.Remove(equipment);
            SaveData();
        }
    }

    /// <summary>
    /// Генерация следующего доступного ID
    /// </summary>
    private int GetNextId() => _equipments.Any() ? _equipments.Max(e => e.Id) + 1 : 1;
}