define(['./api', './post', 'jquery', 'knockout'], function (api, Post, $, ko) {
    function Random(props) {
        this.post = ko.observable({})
        api.getRandomQuestion((e) => {
            this.post(e);
            return e;
        });
    }

    return Random;
})