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
import { photoMain, photoMainB } from "../actions/petActionsCS";
import Link from "next/link";

type Props = {
  adoptions: Adoption;
};


export default function AdoptionsCard({ adoptions }: Props) {
  console.log(adoptions);
  return (
    <>
      {adoptions.map((adoption: Adoption) => (
        <Link href={`adoptions/details/${adoption.id}`} key={adoption.id} >
          <Card className="flex flex-col h-[40vh]" >
            <CardHeader>
              <CardTitle className="flex gap-3">{adoption.name} <p className="text-gray-300">{adoption.gender}</p> </CardTitle>
              <div className="flex gap-1"><p>{adoption.province},</p><p>{adoption.area}</p> </div>
              <CardDescription className="flex justify-center">
                <img src={photoMainB(adoption)} alt="adoption photo" className="w-[300px] rounded-xl" />
              </CardDescription>
            </CardHeader>
            <CardContent></CardContent>
          </Card>

        </Link >))
      }
    </>
  );
}
