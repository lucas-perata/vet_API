"use client"
import React, { useEffect, useState } from 'react';
import { getData } from '../../utils/auth';
import useStore from "../../store/store";

const List: React.FC = () => {
  const [data, setData] = useState(null);
  const token = useStore(state => state.token);

  useEffect(() => {
    getData(token!, "http://localhost:5193/pets")
      .then(data => setData(data))
      .catch(err => console.error(err));
  }, []);

  return (
    <div>{JSON.stringify(data, null, 2)}</div>
  )
}

export default List;