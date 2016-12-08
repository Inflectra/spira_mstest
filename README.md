# Integrate SpiraTeam and Microsoft Visual Studio Team System Test
This code lets you run automated MS-Test tests against a .NET application and have the results be recorded as test runs inside SpiraTest/SpiraTeam.

Please visit [Inflectra](http://www.inflectra.com) for more details about the Spira platform and tools. Feel free to check out the [complete user guide](http://www.inflectra.com/Documents/SpiraTest-Team%20Automated%20Testing%20Integration%20Guide.pdf) for how to integrate SpiraTest or SpiraTeam with automated unit testing tools (including MS-Test).

You can use either SpiraTest® or SpiraTeam® in conjunction with a variety of automated unit testing tools. We assume you are familiar with both SpiraTest/SpiraTeam and the MS-Test.

## Installing the MS-Test Extension
You will need a working copy of SpiraTest/SpiraTeam and Visual Studio Team System 2005 or later / Visual Studio 2008 Professional or later.

The code should be compiled into a binaries (packaged as a .NET dll assembly), each VS folder also includes a sample test.

To use the extension within your test cases, first make sure that the SpiraTestMSTestExtension.dll assembly is added to the project references. You can now use your MS-Test test fixtures with SpiraTest/SpiraTeam.

## Quick Overview of Using the Extension
Check out the [complete user guide](http://www.inflectra.com/Documents/SpiraTest-Team%20Automated%20Testing%20Integration%20Guide.pdf) for details directions.

To use SpiraTest/SpiraTeam with MS-Test, each of the test cases written for execution by MS-Test needs to have a corresponding test case in SpiraTest/SpiraTeam. These can be either existing test cases that have manual test steps or they can be new test cases designed specifically for automated testing and therefore have no defined test steps. In either case, the changes that need to be made to the MS-Test test fixture for SpiraTest to record the MS-Test test run are illustrated below:

```csharp
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension.SampleMSTest
{
 /// <summary>
 /// Sample test fixture that tests the SpiraTest integration
 /// Written by Paul Tissue. Packed by Inflectra Corporation
 /// </summary>
 [
 TestClass
 ]
 public class SpiraTestCaseAttributeTest : MSTestExtensionsTestFixture
 {
 /// <summary>
 /// Test fixture state
 /// </summary>
 protected static int testFixtureState = 1;
 /// <summary>
 /// Constructor method
 /// </summary>
 public SpiraTestCaseAttributeTest()
 {
 //Delegates to base
 //Set the state to 2
 testFixtureState = 2;
 }
 /// <summary>
 /// Sample test that asserts a failure and overrides the default configuration
 /// </summary>
 [
 TestMethod,
 SpiraTestCase(5),
 SpiraTestConfiguration("http://localhost/SpiraTest", "fredbloggs", "fredbloggs", 1, 1, 2)
 ]
 public void SampleFail()
 {
 //Verify the state
 Assert.AreEqual(2, testFixtureState, "*Real Error*: State not persisted");
 //Failure Assertion
 Assert.AreEqual(1, 0, "Failed as Expected");
 }
 /// <summary>
 /// Sample test that succeeds - uses the default configuration
 /// </summary>
 [
 TestMethod,
 SpiraTestCase(6)
 ]
 public void SamplePass()
 {
 //Verify the state
 Assert.AreEqual(2, testFixtureState, "*Real Error*: State not persisted");
 //Successful assertion
 Assert.AreEqual(1, 1, "Passed as Expected");
 }
 /// <summary>
 /// Sample test that does not log to SpiraTest
 /// </summary>
 [
 TestMethod,
 ExpectedException(typeof(AssertFailedException))
 ]
 public void SampleIgnore()
 {
 //Verify the state
 Assert.AreEqual(2, testFixtureState, "*Real Error*: State not persisted");
 //Failure Assertion
 Assert.AreEqual(1, 0, "Failed as Expected");
 }
 }
}
```

And the following settings need to be added to the .config file associated with the test fixture assembly:

```xml
<?xml version="1.0"?>
<configuration>
  <configSections>
    <sectionGroup name="applicationSettings" type="System.Configuration.ApplicationSettingsGroup, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" >
      <section name="Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension.Properties.Settings" type="System.Configuration.ClientSettingsSection, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
    </sectionGroup>
  </configSections>
  <applicationSettings>
    <Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension.Properties.Settings>
      <setting name="Url" serializeAs="String">
        <value>http://localhost/SpiraTest</value>
      </setting>
      <setting name="Login" serializeAs="String">
        <value>fredbloggs</value>
      </setting>
      <setting name="Password" serializeAs="String">
        <value>fredbloggs</value>
      </setting>
      <setting name="ProjectId" serializeAs="String">
        <value>1</value>
      </setting>
      <setting name="ReleaseId" serializeAs="String">
        <value>1</value>
      </setting>
      <setting name="TestSetId" serializeAs="String">
        <value>1</value>
      </setting>
      <setting name="RecordTestRun" serializeAs="String">
        <value>True</value>
      </setting>
    </Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension.Properties.Settings>
  </applicationSettings>

```

Firstly the settings in the .config file indicate the following information to the test fixture:

- The URL to the instance of SpiraTest being accessed. This needs to start with http:// or https://.
- A valid username and password for the instance of SpiraTest.
- The ID of the project (this can be found on the project homepage in the “Project Overview” section)
- (Optional) The ID of the release to associate the test-run with. This can be found on the releases list page (click on the Planning > Releases tab)
- (Optional) The ID of the test set to associate the test-run with. This can be found on the test set list page (click on the Testing > Test Sets tab)
- A flag that tells MS-Test whether or not to record the results in SpiraTest.

Next, the test fixture class needs to be derived from the MSTestExtensionsTestFixture base class so that the test runner knows that the test class will be reporting back to SpiraTest.

In addition, each of the individual test methods needs to be mapped to a specific test case within SpiraTest. This is done by adding a [SpiraTestCase] attribute to the test method together with the ID of the corresponding test case in SpiraTest. The Test Case ID can be found on the test cases list page (click the “Test Cases” tab).

In addition you can also override the default SpiraTest configuration settings from the .config file by adding the [SpiraTestConfiguration] attribute directly to the test method and specifying the SpiraTest URL, authentication information, project id, release id (optional) and test set id (optional). An example of this is shown above for the SampleFail() method. 

Now all you need to do is compile your code, launch Visual Studio, run the test fixtures as you would normally do, and when you view the test cases in SpiraTest, you should see a MSTest automated test run displayed in the list of executed test runs.
