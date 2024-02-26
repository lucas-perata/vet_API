import { serialize } from 'cookie';
import {parseCookies} from "nookies";
import Cookies from 'js-cookie';
import useStore from '@/store/store';


interface LoginData {
    email: string;
    password: string;
}

interface RegisterData extends LoginData {
    displayName: string;
}

export async function login({ email, password }: LoginData) {
  const res = await fetch('http://localhost:5193/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  });

  if (res.ok) {
    const data = await res.json();
    document.cookie = serialize('token', data.token, {
      secure: process.env.NODE_ENV !== 'development',
      sameSite: 'strict',
      maxAge: 3600,
      path: '/',
    });
    return data;
  } else {
    throw new Error('Login failed');
  }
}

export async function registerOwner({ email, password, displayName }: RegisterData) {
  const res = await fetch('http://localhost:5193/register-owner', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password, displayName }),
  });

  if (res.ok) {
    const data = await res.json();
    document.cookie = serialize('token', data.token, {
      secure: process.env.NODE_ENV !== 'development',
      sameSite: 'strict',
      maxAge: 3600,
      path: '/',
    });
    return data;
  } else {
    throw new Error('Registration failed');
  }
}

export async function registerVet({ email, password, displayName }: RegisterData) {
  const res = await fetch('http://localhost:5193/register-vet', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password, displayName }),
  });

  if (res.ok) {
    const data = await res.json();
    document.cookie = serialize('token', data.token, {
      secure: process.env.NODE_ENV !== 'development',
      sameSite: 'strict',
      maxAge: 3600,
      path: '/',
    });
    return data;
  } else {
    throw new Error('Registration failed');
  }
}

export function logout() {
  const removeToken = useStore((state) => state.removeToken);
  removeToken()
}

export async function fetchWithToken(url: string, options: RequestInit = {}, token: string) {
  return fetch(url, {
    ...options,
    headers: {
      ...options.headers,
      Authorization: `Bearer ${token}`,
    },
  });
}


export async function getData(token: string, url: string) {
  const res = await fetchWithToken(url, {}, token);
  if (!res.ok) {
    const text = await res.text();
    console.error('Failed to fetch', res.status, text);
    throw new Error('Failed to fetch');
  }
  return res.json();
}

export function isAuthenticated() {
  const { token } = parseCookies();
  return !!token;
}

export function isAuthenticatedAsync(context : any) {
  const { token } = parseCookies(context);
  return !!token;
}