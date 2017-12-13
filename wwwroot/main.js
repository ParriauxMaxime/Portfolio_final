requirejs.config({
    baseUrl: '',
    paths: {
        jquery: 'scripts/lib/jquery',
        bootstrap: 'scripts/lib/bootstrap',
        knockout: 'scripts/lib/knockout',
        text: 'scripts/lib/text',
    }
})

const routes = ['Home', 'Dashboard', 'Random', 'Favorites']



define(['knockout'], function (ko) {
    const NotFound = {
        viewModel: null,
        template: `<div>Sorry, not working for the moment</div>`
    };

    routes.forEach((elem, i) => {
        const file = elem.toLowerCase();
        const Component = {
            viewModel: {
                require: `viewmodels/${file}`
            },
            template: {
                require: `text!views/${file}.html`
            }
        }
        ko.components.register(elem, Component);
    })

    ko.components.register('Users', NotFound)

    ko.components.register('NotFound', NotFound);

    function Navigation({
        location
    }) {
        this.routes = routes;
        this.active = ko.observable(location.hash === "" ? 'Home' : location.hash.slice('1'));
        this.goTo = (e) => {
            this.active(e);
            location.assign(`#${e}`);
            return false;
        }
    }

    function App() {
        this.navigation = new Navigation({
            location
        });
        window.onhashchange = () => {
            const hash = location.hash.slice(1);
            console.log("New location:", hash)
            if (routes.indexOf(hash) < 0) {
                this.navigation.active("NotFound");
            } else {
                this.navigation.active(hash)
            }
        };
    };

    ko.applyBindings(new App());
})


/*requirejs(['lib/knockout', "posts"], function (ko, posts) {
    ko.applyBindings(posts.AppViewModel);
});*/