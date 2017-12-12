requirejs.config({
    baseUrl: '',
    paths: {
        jquery: 'scripts/lib/jquery',
        bootstrap: 'scripts/lib/bootstrap',
        knockout: 'scripts/lib/knockout',
        text: 'scripts/lib/text',
    }
})

const routes = ['Home', 'Dashboard', 'Random', 'Favorites', 'Search']



define(['knockout'], function(ko) {
    ko.components.register('Home', {
        viewModel: { require: 'viewmodels/home' },
        template: { require: 'text!views/home.html' }
    });

    ko.components.register('404', {
        viewModel: () => ({}),
        template: `<div>Sorry, not working for the moment</div>`
    });

    function Navigation() {
        this.routes = routes;
        this.active = ko.observable(this.routes[0]);
        this.goTo = (e) => {this.active(e); location.assign(`#${e}`); return false;}
    }

    function App() {
        this.navigation = new Navigation();
        window.onhashchange = () => {
            const hash = location.hash.slice(1);
            console.log("New location:", hash)
            this.navigation.active(hash)
        };
        //this.Home = new Home();
    };
    
    ko.applyBindings(new App());
})


/*requirejs(['lib/knockout', "posts"], function (ko, posts) {
    ko.applyBindings(posts.AppViewModel);
});*/