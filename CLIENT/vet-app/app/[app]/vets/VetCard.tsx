import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Vet } from '@/types'
import React from 'react'

function VetCard(props: Vet) {
  return (
    <Card key={props.id} className="w-[350px] bg-green-50">
      <CardHeader>
        <CardTitle>{props.displayName}</CardTitle>
      </CardHeader>
      <CardContent>
        <img src='https://www.asociart.com.ar/wp-content/uploads/2023/10/placeholder-17.png' />
      </CardContent>
      <CardFooter>
        {props.area}
        {props.province}
        {props.email}
      </CardFooter>
    </Card>
  )
}

export default VetCard
