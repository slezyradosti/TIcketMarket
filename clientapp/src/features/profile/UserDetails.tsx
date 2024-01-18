import { observer } from "mobx-react";
import { Card, CardContent, CardDescription, CardHeader, CardMeta, Header, Image } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";

function UserDetails() {
    const { userStore } = useStore();
    const { user } = userStore;

    return (
        <>
            {
                !user
                    ? <><Image src='https://react.semantic-ui.com/images/wireframe/paragraph.png' /></>
                    : (<>
                        <Header as='h3'>User Details</Header>
                        <Card>
                            <Image src='https://react.semantic-ui.com/images/avatar/large/matthew.png' wrapped ui={false} />
                            <CardContent>
                                <CardHeader>{user?.firstname} {user?.lastname}</CardHeader>
                                <CardMeta>
                                    <span className='date'>{user?.dob.toLocaleDateString()}</span>
                                </CardMeta>
                                <CardDescription>
                                    Email: {user?.email}
                                </CardDescription>
                                <CardDescription>
                                    Phone: {user?.phone}
                                </CardDescription>
                            </CardContent>
                            {/* <CardContent extra>
                                <a>
                                    <Icon name='user' />
                                    22 Friends
                                </a>
                            </CardContent> */}
                        </Card>
                    </>)}
        </>
    );
}

export default observer(UserDetails);