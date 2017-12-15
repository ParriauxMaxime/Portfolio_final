define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Random(props) {
        this.post = ko.observable({})
        api.getRandomQuestion((e) => {
            this.post(e);
            return e;
        });
    }

    return Random;
})