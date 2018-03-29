/// <reference name="MicrosoftAjax.js"/>

Type._registerScript("Mock.Sender.Receiver.js", ["IAsyncCommandReceiver.js", "IAsyncCommandSender.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Mock");

Telerik.Sitefinity.Web.UI.Mock.Sender = function(element) {
    Telerik.Sitefinity.Web.UI.Mock.Sender.initializeBase(this, [element]);
};
Telerik.Sitefinity.Web.UI.Mock.Sender.prototype = {

    initialize: function() {
        Telerik.Sitefinity.Web.UI.Mock.Sender.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.Mock.Sender.callBaseMethod(this, "dispose");
    },

    add_command: function(handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function(handler) {
        this.get_events().removeHandler('command', handler);
    },

    // Returns a handler that is to be invoked after the async. request is finished
    _endProcessingHandler: function(sender, args) {
        Sys.Debug.traceDump(sender.get_id(), "receiver id");
        Sys.Debug.traceDump(new Date(), "end");
    },


    _commandHandler: function(sender, args) {
        // handles the command event of CommandWidgets and calles the handlers for the WidgetBar's command event
        var h = this.get_events().getHandler('command');
        Sys.Debug.traceDump(new Date(), "start");
        if (h) h(this, args);
    }

};

Telerik.Sitefinity.Web.UI.Mock.Sender.registerClass('Telerik.Sitefinity.Web.UI.Mock.Sender', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandSender);


Telerik.Sitefinity.Web.UI.Mock.Receiver = function(element) {
    Telerik.Sitefinity.Web.UI.Mock.Receiver.initializeBase(this, [element]);
};
Telerik.Sitefinity.Web.UI.Mock.Receiver.prototype = {
    // set up
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Mock.Receiver.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.Mock.Receiver.callBaseMethod(this, "dispose");
    },

    // Adds a handler that is to be invoked before execution of async command
    add_onStartProcessing: function(handler) { },

    // Removes the star processing handler
    remove_onStartProcessing: function(handler) { },

    // Adds a handler that is invoked when an asyc. command execution is complete
    add_onEndProcessing: function(handler) {
        this.get_events().addHandler('EndProcessing', handler);
    },

    //Removes the end processing handler
    remove_onEndProcessing: function(handler) {
        this.get_events().removeHandler('EndProcessing', handler);
    },

    _endProcessingHandler: function(sender, args) {
        var h = this.get_events().getHandler('EndProcessing');
        if (h) h(this, args);
    },

    // Adds command event listener. Called when a new command event is fired.
    _onCommand: function(commandName, commandArguments) {
        Sys.Debug.traceDump(commandName, "command name");
        Sys.Debug.traceDump(commandArguments, "command args");
        this._endProcessingHandler(this, null);
        //window.setTimeout(Function.createDelegate(this, this._endProcessingHandler), 5000);
    }
};

Telerik.Sitefinity.Web.UI.Mock.Receiver.registerClass('Telerik.Sitefinity.Web.UI.Mock.Receiver', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandReceiver);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
