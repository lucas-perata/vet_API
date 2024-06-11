import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Msg } from '@/types'

function MessagesDash(data) {
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

            {data.data.map((message: Msg) => (
              <TableRow>
                <TableCell>{message.senderUsername}</TableCell>
                <TableCell>{message.messageSent}</TableCell>
              </TableRow>
            ))}

          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}

export default MessagesDash
