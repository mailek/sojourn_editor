using LevelDesigner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LevelDesignerTest
{    
    /// <summary>
    ///This is a test class for LevelObjectDataTest and is intended
    ///to contain all LevelObjectDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LevelObjectDataTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///Tests saving and loading Level definitions
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void LoadSaveLevelFileTest()
        {
            /* TEST DESCRIPTION: A new Level Data target is created with an empty level 
             * definition.  A single object instance is created and placed in the level.
             * The level is then saved.
             * 
             * A second Level Data target is created. This new Level Data is then used 
             * to load the previously saved level definition.  The Object Instance is 
             * then queried for by name.  If found, this newly loaded Object Instance is 
             * then compared to the original.                                           */

            LevelObjectData_Accessor targetSave = new LevelObjectData_Accessor();

            LevelObject expected_lo = targetSave.CreateNewLevelObjectInstance();
            String levelObjectName = "TestingObject";

            /* Set LO properties */
            expected_lo.Name = levelObjectName;
            expected_lo.Index = 99;
            expected_lo.TypeId = Guid.NewGuid();

            expected_lo.Position = new Vector3(-1.0f, 5.0f, -40.0f);
            expected_lo.Rotation = new Vector3(11.0f, -22.0f, 33.0f);
            expected_lo.Scale = new Vector3(-55.0f, 44.0f, -33.0f);

            /* Add the level object to the level definition */
            targetSave.AddObjectToLvl(expected_lo, false);

            /* Save the level definition to disk */
            String outputFileName = "..\\SaveLevelTest.lvl";
            FileStream outStream = File.Open(outputFileName, FileMode.OpenOrCreate, FileAccess.Write);

            targetSave.EnsureLevelDefinition(outStream);
            outStream.Close();

            /* Verify the file was saved */
            Assert.IsTrue(File.Exists(outputFileName));

            /* Load the saved level definition */
            LevelObjectData_Accessor targetLoad = new LevelObjectData_Accessor();
            FileStream inStream = File.OpenRead(outputFileName);

            targetLoad.LoadLevelFile(inStream);
            inStream.Close();

            /* Get the level object from the world */
            LevelObject actual_lo = targetLoad.GetObjectByStringID(levelObjectName);

            /* Verify the properties are present */
            Assert.AreEqual(expected_lo.Name, actual_lo.Name);
            Assert.AreEqual(expected_lo.TypeId, actual_lo.TypeId);
            Assert.AreEqual(expected_lo.Index, actual_lo.Index);
            Assert.AreEqual(expected_lo.Rotation, actual_lo.Rotation);
            Assert.AreEqual(expected_lo.Position, actual_lo.Position);
            Assert.AreEqual(expected_lo.Scale, actual_lo.Scale);

            /* Remove the temporary level file */
            File.Delete(outputFileName);

        }
             
        #region Unimplemented Tests

        /// <summary>
        ///A test for LoadTypeFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void LoadTypeFileTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //Stream stream = null; // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor actual_lo;
            //actual_lo = target.LoadTypeFile(stream);
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LoadTerrainFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void LoadTerrainFileTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //Stream stream = null; // TODO: Initialize to an appropriate value
            //LevelTerrain_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelTerrain_Accessor actual_lo;
            //actual_lo = target.LoadTerrainFile(stream);
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LoadLevelFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void LoadLevelFileTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //Stream stream = null; // TODO: Initialize to an appropriate value
            //target.LoadLevelFile(stream);
            ////Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetTerrain
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetTerrainTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelTerrain_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelTerrain_Accessor actual_lo;
            //actual_lo = target.GetTerrain();
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetObjectTypeList
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetObjectTypeListTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor[] types = null; // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor[] typesExpected = null; // TODO: Initialize to an appropriate value
            //int expected_lo = 0; // TODO: Initialize to an appropriate value
            //int actual_lo;
            //actual_lo = target.GetObjectTypeList(out types);
            //Assert.AreEqual(typesExpected, types);
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetObjectTypeByStringID
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetObjectTypeByStringIDTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //string p = string.Empty; // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor actual_lo;
            //actual_lo = target.GetObjectTypeByStringID(p);
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetObjectByStringID
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetObjectByStringIDTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //string objectID = string.Empty; // TODO: Initialize to an appropriate value
            //LevelObject_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelObject_Accessor actual_lo;
            //actual_lo = target.GetObjectByStringID(objectID);
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetNextTypeIdx
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetNextTypeIdxTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //int expected_lo = 0; // TODO: Initialize to an appropriate value
            //int actual_lo;
            //actual_lo = target.GetNextTypeIdx();
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetNextTerrainIdx
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetNextTerrainIdxTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //int expected_lo = 0; // TODO: Initialize to an appropriate value
            //int actual_lo;
            //actual_lo = target.GetNextTerrainIdx();
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetNextObjectIdx
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void GetNextObjectIdxTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //int expected_lo = 0; // TODO: Initialize to an appropriate value
            //int actual_lo;
            //actual_lo = target.GetNextObjectIdx();
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FetchModel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void FetchModelTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //string modelFileName = string.Empty; // TODO: Initialize to an appropriate value
            //IServiceProvider deviceService = null; // TODO: Initialize to an appropriate value
            //Model model = null; // TODO: Initialize to an appropriate value
            //Model modelExpected = null; // TODO: Initialize to an appropriate value
            //target.FetchModel(modelFileName, deviceService, out model);
            //Assert.AreEqual(modelExpected, model);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EnsureObjectType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void EnsureObjectTypeTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor lot = null; // TODO: Initialize to an appropriate value
            //Stream stream = null; // TODO: Initialize to an appropriate value
            //target.EnsureObjectType(lot, stream);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for EnsureLevelDefinition
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void EnsureLevelDefinitionTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //Stream stream = null; // TODO: Initialize to an appropriate value
            //target.EnsureLevelDefinition(stream);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CreateNewLevelObjectType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void CreateNewLevelObjectTypeTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor actual_lo;
            //actual_lo = target.CreateNewLevelObjectType();
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateNewLevelObjectInstance
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void CreateNewLevelObjectInstanceTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelObject_Accessor expected_lo = null; // TODO: Initialize to an appropriate value
            //LevelObject_Accessor actual_lo;
            //actual_lo = target.CreateNewLevelObjectInstance();
            //Assert.AreEqual(expected_lo, actual_lo);
            ////Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddTerrainToLvl
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void AddTerrainToLvlTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelTerrain_Accessor terr = null; // TODO: Initialize to an appropriate value
            //target.AddTerrainToLvl(terr);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddObjectTypeToPalette
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void AddObjectTypeToPaletteTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelObjectType_Accessor lot = null; // TODO: Initialize to an appropriate value
            //target.AddObjectTypeToPalette(lot);
            ////Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddObjectToLvl
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void AddObjectToLvlTest()
        {
            //LevelObjectData_Accessor target = new LevelObjectData_Accessor(); // TODO: Initialize to an appropriate value
            //LevelObject_Accessor lo = null; // TODO: Initialize to an appropriate value
            //target.AddObjectToLvl(lo);
            ////Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for LevelObjectData Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("LevelDesigner.exe")]
        public void LevelObjectDataConstructorTest()
        {
//            LevelObjectData_Accessor target = new LevelObjectData_Accessor();
////Assert.Inconclusive("Verify the correctness of this test method.");
        }

#endregion
    }
}
