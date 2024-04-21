# Setup RL server

```bash
ssh root@ip
# create user and enable ssh
adduser ecp
passwd ecp
usermod -aG wheel ecp
ssh-keygen
mkdir -p ~/.ssh
cat ~/.ssh/id_rsa.pub >> ~/.ssh/authorized_keys
```

### Add a ansible config file for store credentials
- create a inventory file myHosts.txt
```
16.170.140.133 ansible_ssh_user=root ansible_ssh_pass=sandevdsic123
```
- mkdir `/etc/ansible`
- nano `/etc/ansible/ansible.cfg`
```[defaults]
inventory = /home/ubuntu/myHosts.txt
```
- test connection by
`ansible all -m package -a "name=net-tools state=present"`

### Dynamic variables of a new system

- Port
- MySQL Database / Password
- Backup file

### Install docker on Rocky linux 9

```bash
sudo dnf check-update
sudo dnf config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
sudo dnf install docker-ce docker-ce-cli containerd.io
sudo systemctl start docker
sudo systemctl status docker
# auto restart
sudo systemctl enable docker
# set current user to use docker without sudo
sudo usermod -aG docker $(whoami)
# test 
docker ps
```