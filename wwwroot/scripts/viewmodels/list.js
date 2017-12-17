define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function List(props) {
        this.list = ko.observableArray(props.list)
        this.title = ko.observable(props.title);
        this.link = ko.observable(props.link);
    }

    return List;
})