import React from 'react';
import {
  Column,
  DataGrid,
  Editing,
  RemoteOperations
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import { useStore } from './storeContext';
import DashboardCard from 'src/components/shared/DashboardCard';

const GridEmpleados = () => {
  const { store } = useStore();

  return (
    <DashboardCard title="Tabla Empleados">
      <DataGrid
          dataSource={store}
          showBorders={true}
          repaintChangesOnly={true}
          height={600}
      >
        <Editing
          mode="popup"
          allowAdding={true}
          allowDeleting={true}
          allowUpdating={true}
        />
        <Column dataField="id" caption="ID" allowEditing={false} />
      
        <Column dataField="names" caption="Names" />
        <Column dataField="lastName" caption="Last Name" />
        <Column dataField="secondLastName" caption="Second Last Name" />
        <Column dataField="email" caption="Email" />
      </DataGrid>
    </DashboardCard>
  );
}

export default GridEmpleados;