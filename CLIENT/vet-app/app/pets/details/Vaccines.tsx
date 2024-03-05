"use client";
import React, { useEffect, useState } from "react";
import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { addVaccineToPetCS } from "@/app/actions/petActionsCS";

type Props = {
  id: number;
};

export default function Vaccines({ id }: Props) {
  const [data, setData] = useState(null);
  const [missing, setMissing] = useState(null);
  const [isLoading, setLoading] = useState(true);
  const [refreshKey, setRefreshKey] = useState(0);

  const add = async (petId, vaccineId) => {
    await addVaccineToPetCS({ petId, vaccineId });
    setRefreshKey((oldKey) => oldKey + 1);
  };

  useEffect(() => {
    setLoading(true);

    setTimeout(() => {
      Promise.all([
        fetch(`http://localhost:5193/api/Vaccine/pet/${id}`),
        fetch("http://localhost:5193/api/Vaccine/notpet/4"),
      ]).then(async ([res1, res2]) => {
        const data = await res1.json();
        const missing = await res2.json();
        setData(data);
        setMissing(missing);
        setLoading(false);
      });
    }, 500); // delay of 1 second
  }, [refreshKey]);

  if (isLoading) return <p>Loading...</p>;
  if (!data) return <p>No profile data</p>;

  console.log(missing);

  console.log(data);

  return (
    <div className="flex flex-col gap-4 ">
      {data.map((vaccine) => (
        <Card className="w-[400px] border-green-500">
          <CardHeader>
            <div>{vaccine.name}</div>
            <div>{vaccine.isRequired == true ? "Obligatoria" : "Opcional"}</div>
            <div>{vaccine.sideEffects}</div>
          </CardHeader>
          <CardFooter></CardFooter>
        </Card>
      ))}
      {missing.map((vaccine) => (
        <Card className="w-[400px]">
          <CardHeader>
            <div>{vaccine.name}</div>
            <div>{vaccine.isRequired == true ? "Obligatoria" : "Opcional"}</div>
            <div>{vaccine.sideEffects}</div>
          </CardHeader>
          <CardFooter>
            <div onClick={() => add(id, vaccine.id)}>
              <Button>+</Button>
            </div>
          </CardFooter>
        </Card>
      ))}
    </div>
  );
}
