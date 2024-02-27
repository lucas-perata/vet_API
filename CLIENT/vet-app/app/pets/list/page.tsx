"use client"
import React, { useEffect, useState } from 'react';
import useStore from '@/store/store';
import {createInstance} from '../../../utils/axiosConfig';

const page: React.FC = () => {
  const [data, setData] = useState(null);
  const token = useStore(state => state.token());

  useEffect(() => {
    const instance = createInstance(token);

    instance.get('/pets')
      .then(response => {
        setData(response.data);
      })
      .catch(error => {
        console.error(error);
      });
  }, [token]);

  return (
    <div>{JSON.stringify(data, null, 2)}</div>
  )
}

export default page;