<%@ Page Title="" Language="C#" MasterPageFile="~/Tests/JsTests/tests/TestBase.Master"
    AutoEventWireup="true" CodeBehind="ValidationTest.aspx.cs" Inherits="SitefinityWebApp.JsTests.tests.Telerik.Sitefinity.Web.UI.FieldControls.ValidationTest" %>

<%@ Register Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity"
    TagPrefix="sitefinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TestBody" runat="server">
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="Telerik.Sitefinity.Web.UI.Fields.Validation.Validator.js"
            AssemblyInfo="Telerik.Sitefinity.Abstractions.ObjectFactory" />
    </sitefinity:ResourceLinks>

    <script type="text/javascript">
        var validation;
        var _stringTestValue = 'Something';

        // SetUp and TearDown
        function setUp() {
            validation = new Telerik.Sitefinity.Web.UI.Validation.Validator([]);
        }

        function tearDown() {
        }

        //Tests:

        // --------------------- property tests ----------------------
        function test_get_expectedFormat_returnsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation._expectedFormat = testValue;
            assertEquals(testValue, validation.get_expectedFormat());
        }
        function test_set_expectedFormat_setsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation.set_expectedFormat(testValue);
            assertEquals(testValue, validation._expectedFormat);
        }

        function test_get_maxLength_returnsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation._maxLength = testValue;
            assertEquals(testValue, validation.get_maxLength());
        }
        function test_set_maxLength_setsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation.set_maxLength(testValue);
            assertEquals(testValue, validation._maxLength);
        }

        function test_get_minLength_returnsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation._minLength = testValue;
            assertEquals(testValue, validation.get_minLength());
        }
        function test_set_minLength_setsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation.set_minLength(testValue);
            assertEquals(testValue, validation._minLength);
        }

        function test_get_maxValue_returnsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation._maxValue = testValue;
            assertEquals(testValue, validation.get_maxValue());
        }
        function test_set_maxValue_setsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation.set_maxValue(testValue);
            assertEquals(testValue, validation._maxValue);
        }

        function test_get_minValue_returnsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation._minValue = testValue;
            assertEquals(testValue, validation.get_minValue());
        }
        function test_set_minValue_setsTheValueOfPrivateBackingField() {
            var testValue = Math.random();
            validation.set_minValue(testValue);
            assertEquals(testValue, validation._minValue);
        }

        function test_get_regularExpression_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._regularExpression = testValue;
            assertEquals(testValue, validation.get_regularExpression());
        }
        function test_set_regularExpression_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_regularExpression(testValue);
            assertEquals(testValue, validation._regularExpression);
        }

        function test_get_required_returnsTheValueOfPrivateBackingField() {
            var testValue = true;
            validation._required = testValue;
            assertEquals(testValue, validation.get_required());
        }
        function test_set_required_setsTheValueOfPrivateBackingField() {
            var testValue = true;
            validation.set_required(testValue);
            assertEquals(testValue, validation._required);
        }

        function test_get_alphaNumericViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._alphaNumericViolationMessage = testValue;
            assertEquals(testValue, validation.get_alphaNumericViolationMessage());
        }
        function test_set_alphaNumericViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_alphaNumericViolationMessage(testValue);
            assertEquals(testValue, validation._alphaNumericViolationMessage);
        }

        function test_get_currencyViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._currencyViolationMessage = testValue;
            assertEquals(testValue, validation.get_currencyViolationMessage());
        }
        function test_set_currencyViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_currencyViolationMessage(testValue);
            assertEquals(testValue, validation._currencyViolationMessage);
        }

        function test_get_emailAddressViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._emailAddressViolationMessage = testValue;
            assertEquals(testValue, validation.get_emailAddressViolationMessage());
        }
        function test_set_emailAddressViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_emailAddressViolationMessage(testValue);
            assertEquals(testValue, validation._emailAddressViolationMessage);
        }

        function test_get_integerViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._integerViolationMessage = testValue;
            assertEquals(testValue, validation.get_integerViolationMessage());
        }
        function test_set_integerViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_integerViolationMessage(testValue);
            assertEquals(testValue, validation._integerViolationMessage);
        }

        function test_get_internetUrlViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._internetUrlViolationMessage = testValue;
            assertEquals(testValue, validation.get_internetUrlViolationMessage());
        }
        function test_set_internetUrlViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_internetUrlViolationMessage(testValue);
            assertEquals(testValue, validation._internetUrlViolationMessage);
        }

        function test_get_maxLengthViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._maxLengthViolationMessage = testValue;
            assertEquals(testValue, validation.get_maxLengthViolationMessage());
        }
        function test_set_maxLengthViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_maxLengthViolationMessage(testValue);
            assertEquals(testValue, validation._maxLengthViolationMessage);
        }

        function test_get_maxValueViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._maxValueViolationMessage = testValue;
            assertEquals(testValue, validation.get_maxValueViolationMessage());
        }
        function test_set_maxValueViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_maxValueViolationMessage(testValue);
            assertEquals(testValue, validation._maxValueViolationMessage);
        }

        function test_get_messageCssClass_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._messageCssClass = testValue;
            assertEquals(testValue, validation.get_messageCssClass());
        }
        function test_set_messageCssClass_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_messageCssClass(testValue);
            assertEquals(testValue, validation._messageCssClass);
        }

        function test_get_messageTagName_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._messageTagName = testValue;
            assertEquals(testValue, validation.get_messageTagName());
        }
        function test_set_messageTagName_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_messageTagName(testValue);
            assertEquals(testValue, validation._messageTagName);
        }

        function test_get_minLengthViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._minLengthViolationMessage = testValue;
            assertEquals(testValue, validation.get_minLengthViolationMessage());
        }
        function test_set_minLengthViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_minLengthViolationMessage(testValue);
            assertEquals(testValue, validation._minLengthViolationMessage);
        }

        function test_get_minValueViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._minValueViolationMessage = testValue;
            assertEquals(testValue, validation.get_minValueViolationMessage());
        }
        function test_set_minValueViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_minValueViolationMessage(testValue);
            assertEquals(testValue, validation._minValueViolationMessage);
        }

        function test_get_nonAlphaNumericViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._nonAlphaNumericViolationMessage = testValue;
            assertEquals(testValue, validation.get_nonAlphaNumericViolationMessage());
        }
        function test_set_nonAlphaNumericViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_nonAlphaNumericViolationMessage(testValue);
            assertEquals(testValue, validation._nonAlphaNumericViolationMessage);
        }

        function test_get_numericViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._numericViolationMessage = testValue;
            assertEquals(testValue, validation.get_numericViolationMessage());
        }
        function test_set_numericViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_numericViolationMessage(testValue);
            assertEquals(testValue, validation._numericViolationMessage);
        }

        function test_get_percentageViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._percentageViolationMessage = testValue;
            assertEquals(testValue, validation.get_percentageViolationMessage());
        }
        function test_set_percentageViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_percentageViolationMessage(testValue);
            assertEquals(testValue, validation._percentageViolationMessage);
        }

        function test_get_regularExpressionViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._regularExpressionViolationMessage = testValue;
            assertEquals(testValue, validation.get_regularExpressionViolationMessage());
        }
        function test_set_regularExpressionViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_regularExpressionViolationMessage(testValue);
            assertEquals(testValue, validation._regularExpressionViolationMessage);
        }

        function test_get_requiredViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._requiredViolationMessage = testValue;
            assertEquals(testValue, validation.get_requiredViolationMessage());
        }
        function test_set_requiredViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_requiredViolationMessage(testValue);
            assertEquals(testValue, validation._requiredViolationMessage);
        }

        function test_get_uSocialSecurityNumberViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._uSocialSecurityNumberViolationMessage = testValue;
            assertEquals(testValue, validation.get_uSocialSecurityNumberViolationMessage());
        }
        function test_set_uSocialSecurityNumberViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_uSocialSecurityNumberViolationMessage(testValue);
            assertEquals(testValue, validation._uSocialSecurityNumberViolationMessage);
        }

        function test_get_uSZipCodeViolationMessage_returnsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation._uSZipCodeViolationMessage = testValue;
            assertEquals(testValue, validation.get_uSZipCodeViolationMessage());
        }
        function test_set_uSZipCodeViolationMessage_setsTheValueOfPrivateBackingField() {
            var testValue = getRandomString();
            validation.set_uSZipCodeViolationMessage(testValue);
            assertEquals(testValue, validation._uSZipCodeViolationMessage);
        }

        // --------------------- helper methods ----------------------
        function getRandomString() {
            return String((new Date()).getTime()).replace(/\D/gi, '')
        }
    </script>

</asp:Content>
