<template>
  <div class="auth-test">
    <b-button id="CanAddCustomer" variant="primary" @click="CheckAuthorization"
      >CanAddCustomer</b-button
    >
    <b-button id="CanGetCustomers" variant="primary" @click="CheckAuthorization"
      >CanGetCustomers</b-button
    >
    <b-button id="CanAddProduct" variant="primary" @click="CheckAuthorization"
      >CanAddProduct</b-button
    >
    <b-button id="CanGetProducts" variant="primary" @click="CheckAuthorization"
      >CanGetProducts</b-button
    >
  </div>
</template>

<script>
import { baseApiUrl } from "@/global";
import axios from "axios";

export default {
  name: "AuthorizationTest",
  data: function () {
    return {};
  },
  methods: {
    CheckAuthorization(event) {
      const method = "get";
      const idUser = this.$store.state.user.idUser;
      axios[method](`${baseApiUrl}user/1/checkauthorization/${event.target.id}`)
        .then(() => {
          var msg = { msg: "User have this authorization" };
          this.$toasted.global.defaultSuccess(msg);
        })
        .catch(() => {
          var msg = { msg: "User doesn`t have this authorization" };
          this.$toasted.global.defaultError(msg);
        });
    },
  },
  mounted() {},
};
</script>

<style>
</style>