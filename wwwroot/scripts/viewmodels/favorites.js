define(['jquery', 'knockout', 'api'], function ($, ko, api) {
    function Favorites(props) {

        this.searchResults = ko.observableArray([]);
        this.loading = ko.observable(true);
        this.haveResult = ko.computed(() => this.searchResults().length > 0 || this.loading())
        this.page = ko.observable(0);
        this.prev = ko.observable("");
        this.next = ko.observable("");
        this.pageSize = ko.observable(10);
        this.sizeAviable = ko.observableArray([10, 20, 50])

        this.updateSearch = (page = this.page(), pageSize = this.pageSize()) => {
            api.getFavorites(page, pageSize, favorites => {
                const postIds = favorites.data.map(e => e.data.postId)
                api.getPostsByIds(postIds, e => {
                    const hoc = e.map(e => ({ 
                        ...e,
                        data: { ...e.data,
                            lightView: true,
                            notify: () => {
                                this.updateSearch()
                            }
                        },
                    }))
                    this.searchResults(hoc);
                    this.loading(false);
                    return e;
                });
            });
        }

        this.goPrev = () => {
            this.page(this.page() - 1);
            this.updateSearch();
        }

        this.goNext = () => {
            this.page(this.page() + 1);
            this.updateSearch();
        }

        this.changePageSize = (d, e) => {
            this.pageSize(event.target.value);
            this.updateSearch();
        }

        this.updateSearch();
    }

    return Favorites;
})