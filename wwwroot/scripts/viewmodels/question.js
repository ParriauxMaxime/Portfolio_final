define(['api', 'jquery', 'knockout'], function (api, $, ko) {
  function Question(props) {
      this.question = ko.observable({});
      this.answers = ko.observableArray([]);
      this.updateQuestion = () => {
          let hash = window.location.hash.substring(1).split('/');
          if (hash.length !== 2) return;
          let postId = hash[1];

          api.getPostById(postId, e => {
            this.question(e.data);

            api.getAnswersToPost(e.data.id, answerIds => {
              api.getPostsByIds(answerIds, e => {
                this.answers(e);
                console.log(this.question());
                return e;
              });
            });
          });
      };

      this.updateQuestion();
  }

  return Question;
})