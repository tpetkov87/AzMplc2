<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryBuilderTest.aspx.cs"
    Inherits="SitefinityWebApp.Designers.QueryBuilderTest" %>

<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI"
    TagPrefix="sitefinity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <sitefinity:ResourceLinks ID="resourceLinks" runat="server">
        <sitefinity:ResourceFile Name="CSS/Reset.css" />
        <sitefinity:ResourceFile Name="CSS/Layout.css" />
        <sitefinity:ResourceFile Name="CSS/Colors.css" />
    </sitefinity:ResourceLinks>
    <div class="sfAllToolsWrapper">
        <div class="sfSearch">
            <sitefinity:QueryBuilder ID="qb1" PersistentTypeName="Telerik.Sitefinity.News.Model.NewsItem, Telerik.Sitefinity.Model"
                runat="server" />
            <a href="javascript:serializeQuery()">Serialize</a>
            <div id="result">
            </div>
            <div id="linq">
            </div>
        </div>
    </div>
    </form>

    <script language="javascript" type="text/javascript">
        function serializeQuery() {
            var builder = $find("<%=qb1.ClientID%>");

            var data = builder._queryData;

            var result = Sys.Serialization.JavaScriptSerializer.serialize(data);

            var label = $get("result");
            label.innerHTML = result;

            var reverted = Sys.Serialization.JavaScriptSerializer.deserialize(result);
            var data = DeserializeQueryData(reverted);

            var linqDiv = $get("linq");
            var linq = translateToLinq(data);
            linqDiv.innerHTML = linq;


        }

        function DeserializeQueryData(data) {

            return Telerik.Sitefinity.Web.UI.QueryData.deserialize(data);
            
            
//            var type = Telerik.Sitefinity.Web.UI.QueryData;
//            var result = new type;
//            var items = new Array();
//            for (var i = 0; i < data.QueryItems.length; i++) {
//                var item = data.QueryItems[i];
//                var q_item = $create(Telerik.Sitefinity.Web.UI.QueryDataItem, item, null, null, null);
//                var q_condition = $create(Telerik.Sitefinity.Web.UI.Condition, item.Condition, null, null, null);
//                q_item.set_Condition(q_condition);
//                items.push(q_item)
//                
//            }
//            result.set_QueryItems(items);
//            return result;

        }

        function translateToLinq(queryData, queryItems) {
            var firstItem = true;
            var resultLinq = "";
            if (queryItems == undefined || queryItems == null) {
                queryItems = queryData.getImmediateChildren("");
            }
            
            for (var i in queryItems) {
                var item = queryItems[i];
                if (!firstItem) {
                    resultLinq += " " + item.Join + " ";
                }
                if (!item.IsGroup) {
                    resultLinq+=translateQueryItem(item);
                }
                else {
                    resultLinq += "(";
                    var groupItems = queryData.getImmediateChildren(item.get_ItemPath())
                    resultLinq += translateToLinq(queryData,groupItems);
                    resultLinq += ")";
                }
                firstItem = false;
            }
            return resultLinq;
        }

        function translateQueryItem(queryItem) {
            var condition = queryItem.get_Condition();
            var linq = "";
            linq += condition.FieldName;
            var operator = condition.get_Operator();
            var value = queryItem.get_Value();
            
            switch (operator) {
                case "=":
                case "<":
                case ">":
                case ">=":
                case "<=":
                    linq += condition.get_Operator();
                    linq += wrapQueryValue(condition.get_FieldType(), value);
                    break;
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    linq += "." + operator + "(" + wrapQueryValue(condition.get_FieldType(), value) + ")";
            }
            return linq;
        }

        function wrapQueryValue(valueType, value) {
            switch (valueType) {
                case "Telerik.Sitefinity.Model.Lstring":
                case "System.String":
                    return "\"" + value + "\""; 
                case "System.DateTime":
                    return "("+ value +")";
                case "System.Guid":
                    return "(" + value + ")";
                default:
                    return value;
            }
        }

        function buildSimpleQuery() {
            var result = { "Title": "TestQueryData", "Id": "50CDB5A4-B2F9-46a8-B02F-6B91A6CC5FD2", "QueryItems": [{ "IsGroup": false, "Ordinal": 0, "Join": "AND", "ItemPath": "_0", "Value": "12", "Condition": { "Id": null, "FieldName": "FieldName", "FieldType": "System.String", "Operator": "=", "__msdisposeindex": 3 }, "Name": "Test Group Name", "Id": null, "_itemPathSeparator": "_", "__msdisposeindex": 2 }, { "IsGroup": false, "Ordinal": 1, "Join": "AND", "ItemPath": "_1", "Value": "567", "Condition": { "Id": null, "FieldName": "Title", "FieldType": "System.String", "Operator": "=", "__msdisposeindex": 6 }, "Name": "Test Group Name", "Id": null, "_itemPathSeparator": "_", "__msdisposeindex": 5 }, { "IsGroup": false, "Ordinal": 2, "Join": "AND", "ItemPath": "_2", "Value": "12312", "Condition": { "Id": null, "FieldName": "Author", "FieldType": "System.String", "Operator": "=", "__msdisposeindex": 10 }, "Name": "Test Group Name", "Id": null, "_itemPathSeparator": "_", "__msdisposeindex": 9}], "TypeProperties": ["Summary", "Content", "Author", "SourceName", "SourceSite", "Permissions", "InheritsPermissions", "Urls", "Title", "Description", "UrlName", "DateCreated", "ExpirationDate", "Organizer", "LastModified", "Status", "Category", "Tags"], "_itemPathSeparator": "_", "__msdisposeindex": 1 };


            return result;
        }
    </script>

</body>
</html>
