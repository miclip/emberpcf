import Ember from 'ember';

export default Ember.Route.extend({
    model(){
        return this.get('store').findAll('comment').then(comments=>{
            comments.forEach(function(comment) {
                comment.get('author');
            }, this);
        });
    }
});