"use client";
import * as React from "react";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Adoption } from "@/types";
import { photoMain } from "../actions/petActionsCS";

type Props = {
  adoptions: Adoption;
};

export default function AdoptionsCard({ adoptions }: Props) {
  return (
    <>
      {adoptions.map((adoption: Adoption) => (
        <Card className="flex flex-col h-[40vh]">
          <CardHeader>
            <CardTitle className="flex gap-3">{adoption.pet.name} <p className="text-gray-300">{adoption.pet.gender}</p> </CardTitle>
            <div className="flex gap-1"><p>{adoption.province},</p><p>{adoption.area}</p> </div>
            <CardDescription className="flex justify-center">
              <img src={photoMain(adoption.pet)} alt="adoption photo" className="w-[300px] rounded-xl" />
            </CardDescription>
          </CardHeader>
          <CardContent></CardContent>
        </Card>
      ))}
    </>
  );
}
