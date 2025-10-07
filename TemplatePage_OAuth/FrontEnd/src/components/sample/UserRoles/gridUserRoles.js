import React from 'react';
import {
  DataGrid,
  Editing,
  Column,
  Popup
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useUserRoles } from './userRolesContext'; // Asegúrate de que la ruta sea correcta

const GridUserRoles = () => {
  const { store } = useUserRoles();

  return (
    <DashboardCard title="Tabla de User Roles">
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

        >

        <Popup title="Edit User Roles" showTitle={true} width={700} height={400} />
        </Editing>
      </DataGrid>
    </DashboardCard>
  );
}

export default GridUserRoles;
