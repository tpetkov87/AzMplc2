var detailView;

function OnInsertViewLoaded(sender, args) {
    detailView = sender;
    
    var attributeProductTypesField = findFieldControlByDataFieldName('PostRights', true);

    var toggleAttributeProductTypesDelegate = Function.createDelegate(attributeProductTypesField, toggleAttributeProductTypes);
    attributeProductTypesField.add_valueChanged(toggleAttributeProductTypesDelegate);
}

function findFieldControlByDataFieldName(dataFieldName, required) {
    var fieldControlIds = detailView.get_fieldControlIds();
    var fieldControlsLength = fieldControlIds.length;
    
    while (fieldControlsLength > 0) {
        fieldControlsLength--;
        var fieldControl = $find(fieldControlIds[fieldControlsLength]);
        if(fieldControl.get_dataFieldName() == dataFieldName) {
            return fieldControl;
        }
    }

    if(required) {
        alert('Field control with the DataFieldName "' + dataFieldName + '" cannot be found and it is required.');
    }

    return null;
}

function toggleAttributeProductTypes(target) {
    var specificChoicesField = findFieldControlByDataFieldName('PostRightsMy', true);

    if (target.get_selectedChoicesIndex() == 0) {
        $(specificChoicesField._element).hide();
    } else {
        $(specificChoicesField._element).show();
    }
}