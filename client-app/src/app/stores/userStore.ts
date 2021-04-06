import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  // computed prop
  get isLoggedIn() {
    // cat the user object as a boolean
    return !!this.user;
  }

  login = async (credentials: UserFormValues) => {
    try {
      const user = await agent.Account.login(credentials);

      store.commonStore.setToken(user.token);

      runInAction(() => (this.user = user));

      history.push("/activities");

      // console.log(user);
      // close modal once user logins
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  };

  logout = () => {
    store.commonStore.setToken(null);
    window.localStorage.removeItem("jwt");
    this.user = null;
    history.push("/");
  };

  getUser = async () => {
    try {
      const user = await agent.Account.current();
      runInAction(() => {
        this.user = user;
      });
    } catch (error) {
      console.log(error);
    }
  };

  register = async (credentials: UserFormValues) => {
    try {
      const user = await agent.Account.register(credentials);
      store.commonStore.setToken(user.token);
      runInAction(() => (this.user = user));
      history.push("/activities");
      store.modalStore.closeModal();


      
    } catch (error) {
      throw error;
    }
  };
}
