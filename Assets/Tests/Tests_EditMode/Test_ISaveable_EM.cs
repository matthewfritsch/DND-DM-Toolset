using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.TestTools;

/// <summary>
/// This is the main testing component
/// </summary>
public class Test_ISaveable_EM {
    
    /// <summary>
    /// This tests creating save data from an object, checking to see if it returns anything.
    /// </summary>
    [Test]
    public void CreateSaveData_WithFakeInitiativeQueueOfStrings_ShouldReturnAnything() {
        // initialize a queue
        FakeListOfStrings queue = new FakeListOfStrings();

        // let it create the data
        string saveData = queue.CreateSaveData();

        // check to see if it isn't null
        Assert.IsNotNull(saveData);
    }

    /// <summary>
    /// This tests creating an object that adds new, non-default data, and tries
    /// to create a new object from it. Both objects should be the same.
    /// </summary>
    [Test]
    public void CreateSaveData_WithFakeInitiativeQueueOfStringsAndNewData_ShouldCreateNewObjectWithSameData() {
        // Initialize a queue
        FakeListOfStrings queue = new FakeListOfStrings();

        // add some new non-default data
        queue.queue.Add("Some new data!");

        // let it create the data
        string saveData = queue.CreateSaveData();

        // then let it try to load the data
        FakeListOfStrings loadedQueue = JsonUtility.FromJson<FakeListOfStrings>(saveData);

        // check if the loaded queue is the same as the original
        Assert.AreEqual(queue.queue, loadedQueue.queue);
        // also check if the loaded queue is not equal to a brand new creation
        Assert.AreNotEqual(new FakeListOfStrings().queue, loadedQueue);
    }

    /// <summary>
    /// This tests if the overridden function returns anything
    /// </summary>
    [Test]
    public void CreateSaveData_WithFakeListOfFakeObjects_ShouldReturnAnything() {
        // initialize the list
        FakeListOfFakeObjects list = new FakeListOfFakeObjects();

        // let it create the data
        string saveData = list.CreateSaveData();

        // check to see if it returned anything
        Assert.IsNotNull(saveData);
    }

    /// <summary>
    /// This tests if the data that is saved can be used to create an exact copy of the object.
    /// </summary>
    [Test]
    public void CreateSaveData_WithFakeListOfFakeObjects_ShouldCreateNewObjectWithSameData() {
        // initialize the list
        FakeListOfFakeObjects list = new FakeListOfFakeObjects();

        // let it create the data
        string saveData = list.CreateSaveData();
        // create a new object using this data
        FakeListOfFakeObjects loadedList = JsonUtility.FromJson<FakeListOfFakeObjects>(saveData);

        // assert that these two hold the same data
        Assert.AreEqual(list.data, loadedList.data);
    }

    /// <summary>
    /// This tests if new data can be added to a list, and still load the same data
    /// </summary>
    [Test]
    public void CreateSaveData_WithFakeListOfFakeObjectsWithNewData_ShouldCreateNewObjectWithSameData() {
        // initialize the list
        FakeListOfFakeObjects list = new FakeListOfFakeObjects();
        // add some new non-default data
        list.data.Add(new FakeListOfFakeObjects.FakeInfo("NewMonster", MP: 5d, HP: 100d));

        // let it create the data
        string saveData = list.CreateSaveData();
        // create a new object using this data
        FakeListOfFakeObjects loadedList = JsonUtility.FromJson<FakeListOfFakeObjects>(saveData);

        // assert that these two hold the same data
        Assert.AreEqual(list.data, loadedList.data);
        // also check that this new data is not the same as a newly constructed object
        Assert.AreNotEqual(loadedList.data, new FakeListOfFakeObjects().data);
    }
}

// =================================================
// Below are mock classes used for testing

// mock class for testing, serializing a list of strings
[System.Serializable]
class FakeListOfStrings : ISaveable {
    public List<string> queue;

    public FakeListOfStrings() {
        // initialize the list
        queue = new List<string>();

        // add some dummy data
        queue.Add("\"mname\", \"mtype\", \"malignment\", Size.GARGANTUAN, 10d, 15, 20, 15, 30, 28, 23, 19, 12)");
        queue.Add("\"mname2\", \"mtype2\", \"malignment2\", Size.GARGANTUAN, 10d, 15, 20, 15, 30, 28, 23, 19, 12)");
    }

    // turn the list into a json string
    public string CreateSaveData() {

        return JsonUtility.ToJson(this);
    }

    public void PopulateSaveData(SaveData sd) {}
}

// mock class for testing, serializing a list of non-primitive objects
[System.Serializable]
class FakeListOfFakeObjects : ISaveable {
    /// <summary>
    /// Class that contains basic data that can be constructed with a string and two doubles
    /// </summary>
    [System.Serializable]
    public class FakeInfo {
        [SerializeField]
        string name;
        [SerializeField]
        protected double HP;
        [SerializeField]
        private double MP;

        public FakeInfo(string name = "Monster", double HP = 0d, double MP = 0d) {
            this.name = name;
            this.HP = HP;
            this.MP = MP;
        }

        // Both functions below were auto-generated by visual studio. The reason that this
        //  is needed is because this is used in Lists when checking equality, and for our use case,
        //  equality is done simply by checking if the fields are equal
        public override bool Equals(object obj) {
            return obj is FakeInfo info &&
                   name == info.name &&
                   HP == info.HP &&
                   MP == info.MP;
        }
        public override int GetHashCode() {
            var hashCode = -18802869;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + HP.GetHashCode();
            hashCode = hashCode * -1521134295 + MP.GetHashCode();
            return hashCode;
        }
    }

    // FIELDS
    public List<FakeInfo> data;

    // CONSTRUCTORS
    public FakeListOfFakeObjects() {
        // construct new list
        data = new List<FakeInfo>();

        // add dummy data
        data.Add(new FakeInfo("Monster1", 100d, 40d));
        data.Add(new FakeInfo("Monster2", 300d, 10d));
        data.Add(new FakeInfo("Monster3", 85d, 125d));
        data.Add(new FakeInfo("Boss", 500d, 125d));
    }

    public string CreateSaveData() {
        return JsonUtility.ToJson(this);
    }

    public void PopulateSaveData(SaveData sd) {}
    
}
