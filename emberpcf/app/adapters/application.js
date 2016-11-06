import DS from 'ember-data';

export default DS.JSONAPIAdapter.extend({
  headers: {
    
  },
  host: 'http://emberpcf-api.cfapps.io'
});