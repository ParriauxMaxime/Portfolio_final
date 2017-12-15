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

const routes = ['Home', 'Dashboard', 'Random', 'Favorites']

// TODO: this is supposed to navigate to the search part
const search = function (formElement) {
    console.log(formElement);
}

define(['knockout', 'api'], function (ko, api) {
    const NotFound = {
        viewModel: null,
        template: `<div>Sorry, not working for the moment</div>`
    };

    ko.components.register('Post', {
        viewModel: {
            require: 'scripts/viewmodels/post'
        },
        template: {
            require: 'text!views/post.html'
        }
    });

    ko.components.register('PostView', {
        viewModel: function PostView(props) {
            this.post = ko.observable({});
            const [hash, id] = location.hash.slice(1).split('/')
            if (id) {
                api.getPostById(id, (e) => {
                    console.log(e);
                    this.post(e.data);
                    return (e);
                })
            }
        },
        template: `
            <div class="no-gutters grey-bc min-fh" id="Random">
            <div data-bind="text: console.log(post())"/>
            <div data-bind="component: { name: 'Post', params: post() }">        
            </div>    
            `

    });

    [...routes, 'Search'].forEach((elem, i) => {
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
    }

    function App() {
        this.navigation = new Navigation();
        const navigationOG = () => {
            const hash = location.hash.slice(1);
            if (routes.indexOf(hash) < 0) {
                switch (hash.split('/')[0]) {
                    case 'Search':
                        {
                            break;
                        }
                    case 'Post':
                        {
                            return 'PostView'
                            break;
                        }
                    default:
                        {
                            return ("NotFound");
                            break;
                        }
                }
            } else {
                return (hash.split('/')[0])
            }
        }
        this.navigation.active(navigationOG())
        window.onhashchange = () => {
            this.navigation.active(navigationOG())
        }

    };

    ko.applyBindings(new App());
})


/*requirejs(['lib/knockout', "posts"], function (ko, posts) {
    ko.applyBindings(posts.AppViewModel);
});*/