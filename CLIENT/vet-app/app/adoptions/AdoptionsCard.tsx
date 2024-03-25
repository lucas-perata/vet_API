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

type Props = {
  adoptions: Adoption;
};

export default function AdoptionsCard({ adoptions }: Props) {
  return (
    <>
      {adoptions.map((adoption: Adoption) => (
        <Card className="flex h-[25vh] w-[23vw]">
          <CardHeader>
            <CardTitle>{adoption.pet.name}</CardTitle>
            <CardDescription>
              {adoption.area}
              {adoption.province}
              {adoption.pet.gender}
            </CardDescription>
          </CardHeader>
          <CardContent></CardContent>
          <CardFooter className="flex justify-between">
            <Button variant="outline">Cancel</Button>
            <Button>Deploy</Button>
          </CardFooter>
        </Card>
      ))}
    </>
  );
}
