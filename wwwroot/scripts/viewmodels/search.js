define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Search(props) {
        const query = api.getSearchQuery;

        this.searchResults = ko.observableArray([]);
        this.loading = ko.observable(true);
        this.query = ko.observable(query());
        this.haveResult = ko.computed(() => this.searchResults().length > 0 || this.loading())
        this.encodedQuery = ko.computed(() => encodeURIComponent(this.query()));

        this.updateSearch = (query = query()) => {
            this.query(query)
            console.log(query.replace(/\s+/g, ','));
            api.getSearchResults(query.replace(/\s+/g, ','), true, postIds => {
                api.getPostsByIds(postIds, e => {
                    this.searchResults(e);
                    this.loading(false);
                    return e;
                });
            });
        }
        const t = location.hash.slice(1).split('/')
        if (t.length > 1) {
            this.updateSearch(t[1]);
        }
    }

    return Search;
})