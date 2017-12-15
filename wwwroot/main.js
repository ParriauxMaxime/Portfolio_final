requirejs.config({
    baseUrl: '',
    paths: {
        jquery: 'scripts/lib/jquery',
        api: 'scripts/api',
        bootstrap: 'scripts/lib/bootstrap',
        knockout: 'scripts/lib/knockout',
        text: 'scripts/lib/text',
        post: 'scripts/viewmodels/post'
    }
})

const routes = ['Home', 'Dashboard', 'Random', 'Favorites'];
const hiddenPages = ['Search', 'Question'];

define(['knockout'], function (ko) {
    const NotFound = {
        viewModel: null,
        template: `<div>Sorry, not working for the moment</div>`
    };

    ko.components.register('Post', {
        viewModel: {
            require: `scripts/viewmodels/post`
        },
        template: {
            require: 'text!views/post.html'
        }
    });

    [ ...routes, ...hiddenPages].forEach((elem, i) => {
        const file = elem.toLowerCase();
        const Component = {
            viewModel: {
                require: `scripts/viewmodels/${file}`
            },
            template: {
                require: `text!views/${file}.html`
            }
        }
        ko.components.register(elem, Component);
    })

    ko.components.register('Users', NotFound)

    ko.components.register('NotFound', NotFound);

    function Navigation() {
        this.routes = routes;
        const hash = location.hash.slice(1);
        this.active = ko.observable(hash === "" ? 'Home' : hash);
        this.goTo = (e) => {
            this.active(e);
            location.assign(`#${e}`);
            return false;
        }
        this.search = (formElement) => {
            this.active("Search");
            // This is needed when we are already in the search page
            // Otherwise observable doesn't trigger
            this.active.valueHasMutated();
        }
    }

    function App() {
        this.navigation = new Navigation();
        window.onhashchange = () => {
            const hash = location.hash.slice(1).split('/')[0];
            if (hiddenPages.indexOf(hash) < 0 && routes.indexOf(hash) < 0) {
                this.navigation.active("NotFound");
            } else {
                this.navigation.active(hash);
            }
        };
    };

    ko.applyBindings(new App());
})


/*requirejs(['lib/knockout', "posts"], function (ko, posts) {
    ko.applyBindings(posts.AppViewModel);
});*/