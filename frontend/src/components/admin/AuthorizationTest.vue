<template>
  <div class="auth-test">
    <b-button
      class="mr-2"
      id="CanAddCustomer"
      variant="primary"
      @click="CheckAuthorization"
      >AddCustomer</b-button
    >
    <b-button
      class="mr-2"
      id="CanGetCustomers"
      variant="primary"
      @click="CheckAuthorization"
      >GetCustomers</b-button
    >
    <b-button
      class="mr-2"
      id="CanAddProduct"
      variant="primary"
      @click="CheckAuthorization"
      >AddProduct</b-button
    >
    <b-button id="CanGetProducts" variant="primary" @click="CheckAuthorization"
      >GetProducts</b-button
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
      axios[method](
        `${baseApiUrl}user/${idUser}/checkauthorization/${event.target.id}`
      )
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