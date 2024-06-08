import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"

function MessagesDash() {
  return (
    <Card className="h-[35vh] w-72">
      <CardHeader>
        <CardTitle>Mensajes</CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Usuario</TableHead>
              <TableHead>Recibido (fecha)</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow>
              <TableCell>Pepito</TableCell>
              <TableCell>Hace 5 horas...</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Pepito</TableCell>
              <TableCell>Hace 5 horas...</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Pepito</TableCell>
              <TableCell>Hace 5 horas...</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Pepito</TableCell>
              <TableCell>Hace 5 horas...</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Pepito</TableCell>
              <TableCell>Hace 5 horas...</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Pepito</TableCell>
              <TableCell>Hace 5 horas...</TableCell>
            </TableRow>

          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}

export default MessagesDash
