import create from 'zustand';
import Cookies from 'js-cookie';

type State = {
  token: () => string | undefined;
  setToken: (token: string) => void;
  removeToken: () => void;
};

const useStore = create<State>(set => ({
  token: () => Cookies.get('token'),
  setToken: (token: string) => {
    Cookies.set('token', token);
    set({ token: () => Cookies.get('token') });
  },
  removeToken: () => {
    Cookies.remove('token');
    set({ token: () => undefined });
  },
}));


export default useStore;