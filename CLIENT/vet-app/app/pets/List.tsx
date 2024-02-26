"use client"
import React, { useEffect, useState } from 'react';
import { parseCookies } from 'nookies';
import { fetchWithToken, getData } from '../../utils/auth';

export default function List() {
  const [data, setData] = useState(null);

  useEffect(() => {
    const cookies = parseCookies();
    getData(cookies.token, "http://localhost:5193/pets")
      .then(data => setData(data))
      .catch(err => console.error(err));
  }, []);

  return (
    <div>{JSON.stringify(data, null, 2)}</div>
  )
}