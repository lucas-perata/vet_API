import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"


function ReviewsDash() {
  return (
    <Card className="h-[35vh] w-[35vw]">
      <CardHeader>
        <CardTitle>Últimas reseñas</CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Fecha</TableHead>
              <TableHead>Estrellas</TableHead>
              <TableHead>Descripción</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>5</TableCell>
              <TableCell>Servicio iorem ipsuom servicium iorem</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>3</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>4</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>4</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>4</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
            </TableRow>
          </TableBody>
        </Table>

      </CardContent>
    </Card>
  )
}

export default ReviewsDash
