import DS from 'ember-data';

export default DS.Model.extend({
   title: DS.attr('string'),
   author: DS.belongsTo('person'),
   comments: DS.hasMany('comment'),
   body: DS.attr('string')
});