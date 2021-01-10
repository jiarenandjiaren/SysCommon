<template>
  <div>
    跳转中，请稍等...
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'OidcRedirect',
  data: function() {
return {
    ...mapGetters([
      'oidcIsAuthenticated'
    ])
  };
},
  created() {
    if (this.$store.getters.oidcUser) {
      this.$router.push('/')
    }
  }
}

</script>
