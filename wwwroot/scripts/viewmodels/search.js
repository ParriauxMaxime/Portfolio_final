define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Search(props) {
        this.searchResults = ko.observableArray([]);
        this.loading = ko.observable(true);

        this.updateSearch = () => {
            let query = api.getSearchQuery();
            query = encodeURIComponent(query);

            api.getSearchResults(query, true, postIds => {
                api.getPostsByIds(postIds, e => {
                    for (let k of e)
                        k.data.lightView = true;
                    
                    this.searchResults(e);
                    this.loading(false);
                    return e;
                });
            });
        }
        this.updateSearch();
    }

    return Search;
})