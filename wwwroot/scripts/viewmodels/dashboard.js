define(['jquery', 'knockout', 'api'], function ($, ko, api) {
    function Dashboard(props) {
        //link, text(), creationDate
        this.userCard = ko.observable({});
        this.topScoreCard = ko.observable({});
        this.userPage = ko.observable(0);
        this.topScorePage = ko.observable(0);
        this.userPageSize = ko.observable(10);
        this.topScorePageSize = ko.observable(10);

        this.updateUserCard = (page = 0, pageSize = 50) => {
            return api.getUsers(page, pageSize, (res) => {
                const hoc = {
                    ...res,
                    mapping: e => {
                        return {
                            ...e.data,
                            text: ko.computed(() => e.data.displayName),
                            score: -1,
                        }
                    },
                    pageSize: this.userPageSize(),
                    list: res.data,
                    pagination: true,
                    link: 'User',
                    goPrev: () => {
                        this.userPage(this.userPage() - 1)
                        this.updateUserCard(this.userPage(), this.userPageSize())
                    },
                    goNext: () => {
                        this.userPage(this.userPage() + 1)
                        this.updateUserCard(this.userPage(), this.userPageSize())                        
                    },
                    changePageSize: (param = 50) => {
                        this.userPageSize(param)
                        this.updateUserCard(this.userPage(), this.userPageSize())
                    }
                }
                this.userCard(hoc);
            })
        }

        this.updateTopScore = (page = 0, pageSize = 50) => {
            return api.getQuestionsByScore(page, pageSize, (res) => {
                const hoc = {
                    ...res,
                    mapping: e => {
                        return {
                            ...e.data,
                            text: ko.computed(() => e.data.title),
                        }
                    },
                    pageSize: this.topScorePageSize(),
                    list: res.data,
                    pagination: true,
                    link: 'Question',
                    goPrev: () => {
                        this.topScorePage(this.topScorePage() - 1)
                        this.updateTopScore(this.topScorePage(), this.topScorePageSize())
                    },
                    goNext: () => {
                        this.topScorePage(this.topScorePage() + 1)
                        this.updateTopScore(this.topScorePage(), this.topScorePageSize())                        
                    },
                    changePageSize: (param = 50) => {
                        this.topScorePageSize(param)
                        this.updateTopScore(this.topScorePage(), this.topScorePageSize())
                    }
                }
                this.topScoreCard(hoc);
            })
        }

        this.updateDashboard = () => {
            this.updateUserCard(this.userPage(), this.userPageSize());
            this.updateTopScore(this.topScorePage(), this.topScorePageSize());
        }

        this.updateDashboard();

    }

    return Dashboard;
})