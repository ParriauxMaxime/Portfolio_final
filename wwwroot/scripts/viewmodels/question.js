define(['./api', 'jquery', 'knockout'], function (api, $, ko) {
  function Question(props) {
      this.question = ko.observable({});
      this.answers = ko.observableArray([]);
      this.updateQuestion = () => {
          let postId = +window.location.hash.substring(1);

          if (postId === NaN) return;

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