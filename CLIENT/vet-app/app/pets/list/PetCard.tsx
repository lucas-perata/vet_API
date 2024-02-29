import React from 'react'
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/ui/avatar"

type Props = {
    pet: any,
    photo: any,
}

export default function PetCard({pet, photo}: Props) {
  return (
  <> 
    <Card className="w-[700px] h-[400px]  hover:shadow-xl">
      <CardHeader className='flex flex-row'>
        <Avatar className='w-[300px] h-[300px] shadow-lg'>
          <AvatarImage src={photo.find((main) => main.isMain == true)?.url || ""} alt="foto-mascota" />
          <AvatarFallback>Subí una foto</AvatarFallback>
        </Avatar>
        <div className='flex flex-col gap-4 p-4'>
          <CardTitle>{pet.name}</CardTitle>
          <CardDescription>Raza: {pet.breed}</CardDescription>
          <CardDescription>Fecha de nacimiento {pet.dateOfBirth}</CardDescription>
          <CardDescription>Color {pet.color}</CardDescription>
          <CardDescription>Género {pet.gender}</CardDescription>
          <CardDescription>{pet.isNeutered == true ? "Castrado" : null}</CardDescription>
        </div>
      </CardHeader>
      <CardFooter className="flex justify-end gap-4">
        <Button variant="outline">Eliminar</Button>
        <Button>Historial médico</Button>
        <Button>Editar</Button>
      </CardFooter>
    </Card>
  </>    
  )
}
