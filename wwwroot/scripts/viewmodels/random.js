define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Random(props) {
        this.loading = ko.observable(true);      
        this.postId = ko.observable(undefined);
        api.getRandomQuestion((e) => {
            this.postId(e.id);
            this.loading(false);            
            return e;
        });
    }

    return Random;
})