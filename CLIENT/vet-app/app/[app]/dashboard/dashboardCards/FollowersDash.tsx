import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Follower } from '@/types';

function FollowersDash(data) {
  console.log(data);
  return (
    <Card className="h-[35vh] w-72">
      <CardHeader>
        <CardTitle>Nuevos seguidores</CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Usuario</TableHead>
              <TableHead></TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>


            {data.data.map((follower: Follower) => (
              <TableRow>
                <TableCell>{follower.username}</TableCell>
                <TableCell>{follower.date}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}

export default FollowersDash;
