define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Search(props) {
        const query = api.getSearchQuery;

        this.searchResults = ko.observableArray([]);
        this.loading = ko.observable(true);
        this.query = ko.observable(query());
        this.haveResult = ko.computed(() => this.searchResults().length > 0 || this.loading())
        this.encodedQuery = ko.computed(() => encodeURIComponent(this.query()));
        this.page = ko.observable(0);
        this.prev = ko.observable("");
        this.next = ko.observable("");
        this.pageSize = ko.observable(10);
        this.sizeAviable = ko.observableArray([10, 20, 50])

        this.updateSearch = (page = this.page(), pageSize = this.pageSize(), query = this.query()) => {
            this.query(query)
            api.getSearchResults(page, pageSize, query.replace(/\s+/g, ','), true, postIds => {
                api.getPostsByIds(postIds, e => {
                    const hoc = e.map(e => ({...e, data: {...e.data, lightView: true}}))
                    this.searchResults(hoc);
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