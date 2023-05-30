import React, { useEffect, useState } from 'react';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import agent from '../api/agent';
import LoadingComponent from './loadingComponents';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';

function App() {
  const {activityStore} = useStore();

  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore])

if(activityStore.loadingInitial) return <LoadingComponent content='Loading Activities...'/>

  return(
    <>
      <NavBar/>
      <Container style={{marginTop : '7em'}}>
        <ActivityDashboard/>
      </Container>
    </>
  );
}

export default observer(App);